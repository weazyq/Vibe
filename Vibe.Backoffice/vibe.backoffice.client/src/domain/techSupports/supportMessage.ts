export class SupportMessage {
    constructor(
        public id: string,
        public text: string,
        public createdAt: Date,
        public modifiedAt: Date | null,
        public createdBy: string
    ) {}
}

export function mapToSupportMessage(data: any): SupportMessage {
    const createdAt = new Date(data.createdAt)
    const modifiedAt = data.modifiedAt != null ? new Date(data.modifiedAt) : null

    return new SupportMessage(data.id, data.text, createdAt, modifiedAt, data.createdBy)
}

export function mapToSupportMessages(data: any[]): SupportMessage[] {
    return data.map(d => mapToSupportMessage(d))
}