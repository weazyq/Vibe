export class SupportRequest{
    constructor(
        public id: string,
        public title: string,
        public description: string,
        public clientId: string,
        public employeeId: string | null,
        public openedAt: Date,
        public isClosed: boolean
    ) { }
}

export function mapToSupportRequest(data: any): SupportRequest {
    return new SupportRequest(data.id, data.title, data.description, data.clientId, data.employeeId, new Date(data.openedAt), 
        data.isClosed)    
}

export function mapToSupportRequests(data: any[]): SupportRequest[] {
    return data.map(d => mapToSupportRequest(d))
}