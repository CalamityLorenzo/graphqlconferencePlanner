﻿using ConferencePlanner.GraphQL.Common;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Sessions
{
    [ExtendObjectType("Mutation")]
    public class SessionMutation
    {
        public async Task<AddSessionPayload> AddSessionAsync(
            AddSessionInput input,
            [ScopedService] ApplicationDbContext ctx,
            CancellationToken ct)
        {
            if (string.IsNullOrEmpty(input.Title)) return new AddSessionPayload(new UserError("The title cannot be empty.", "TITLE_EMPTY"));
            if (input.SpeakerIds.Count == 0) return new AddSessionPayload(new UserError("No speaker assigned.", "NO_SPEAKER"));

            var session = new Session
            {
                Title = input.Title,
                Abstract = input.Abstract
            };

            foreach (int speakerId in input.SpeakerIds)
                session.SessionSpeakers.Add(new SessionSpeaker
                {
                    SpeakerId = speakerId
                });
            ctx.Sessions.Add(session);

            await ctx.SaveChangesAsync(ct);

            return new AddSessionPayload(session);
        }

        [UseApplicationDbContext]
        public async Task<ScheduleSessionPayload> ScheduleSessionAsync(
            ScheduleSessionInput input, 
            [ScopedService] ApplicationDbContext ctx)
        {
            if (input.EndTime < input.StartTime)
                return new ScheduleSessionPayload(new UserError("endTime has to be larger than startTime", "END_TIME_INVALID"));

            Session? session = await ctx.Sessions.FindAsync(input.SessionId);
            int? initialTrackId = session?.TrackId;

            if (session is null) return new ScheduleSessionPayload(new UserError("Session not found", "SESSION_NOT_FOUND"));

            session.TrackId = input.TrackId;
            session.StartTime = input.StartTime;
            session.EndTime = input.EndTime;

            await ctx.SaveChangesAsync();

            return new ScheduleSessionPayload(session);
        }
    }
}
