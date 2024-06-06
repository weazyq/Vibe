export class EmployeeBlank{
    constructor(
        public name: string | null, 
        public phone: string | null,
        public login: string | null,
        public password: string | null,
        public email: string | null, 
    ) {}

    public static getDefault(): EmployeeBlank {
        return new EmployeeBlank(null, null, null, null, null)
    }
}