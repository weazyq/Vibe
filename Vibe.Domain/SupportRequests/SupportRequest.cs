﻿namespace Vibe.Domain.SupportRequests
{
    public class SupportRequest
    {
        public Guid Id { get; }
        public String Title { get; }
        public String Description { get; }
        public Guid ClientId { get; }
        public Guid? EmployeeId { get; }
        public DateTime OpenedAt { get; set; }
        public Boolean IsClosed { get; }

        public SupportRequest(Guid id, String title, String description, Guid clientId, Guid? employeeId,
            DateTime openedAt, Boolean isClosed)
        {
            Id = id;
            Title = title;
            Description = description;
            ClientId = clientId;
            EmployeeId = employeeId;
            OpenedAt = openedAt;
            IsClosed = isClosed;
        }
    }
}
