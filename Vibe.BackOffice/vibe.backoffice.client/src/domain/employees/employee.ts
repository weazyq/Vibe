export class Employee {
    constructor(
        public id: string,
        public name: string, 
        public phone: string,
        public login: string,
        public email: string, 
    ) {}

}

export function mapToEmployee(data: any): Employee {
    return new Employee(data.id, data.name, data.phone, data.login, data.email)
}

export function mapToEmployees(data: any[]): Employee[] {
    return data.map(d => mapToEmployee(d))
}