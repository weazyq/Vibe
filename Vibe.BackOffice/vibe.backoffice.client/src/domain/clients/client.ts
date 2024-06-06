export class Client {
    constructor(
        public id: string,
        public name: string,
        public phone: string,
    ) { }
}

export function mapToClient(data: any): Client {
    return new Client(
        data.id,
        data.name,
        data.phone
    )
}