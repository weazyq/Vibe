import { Client, mapToClient } from "../clients/client";
import { SupportMessage, mapToSupportMessages } from "./supportMessage";

export class SupportRequestDetail {
    constructor(
        public id: string,
        public title: string,
        public description: string,
        public client: Client,
        public employeeId: string | null,
        public openedAt: Date,
        public isClosed: boolean,
        public messages: SupportMessage[] = []
    ) { }
}

export function mapToSupportRequestDetail(data: any): SupportRequestDetail {
    const messages: SupportMessage[] = mapToSupportMessages(data.messages)

    const client = mapToClient(data.client)
    
    return new SupportRequestDetail(data.id, data.title, data.description, client, data.employeeId,
        new Date(data.openedAt), data.isClosed, messages)
}