import axios from "axios";
import { Employee, mapToEmployees } from "./employee";
import Result from "../../tools/result";

export class EmployeeProvider {
    static async list(): Promise<Employee[]> {
        const token = localStorage.getItem('token')
        
        const response = await axios.get('/Employees/List', {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        return mapToEmployees(response.data)
    }

    static async delete(employeeId: string): Promise<Result>{
        const token = localStorage.getItem('token')

        const response = await axios.get('/Employees/Delete', {
            params: {
                employeeId
            },
            headers: {
                Authorization: `Bearer ${token}`
            }
        })

        if(!response.data.isSuccess) return Result.fail(response.data.errors[0])
        return Result.success(null)
    }
}