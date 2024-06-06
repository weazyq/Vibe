import axios from "axios";
import { LoginResultDTO } from "./loginResultDTO";
import Result from "../../tools/result";

export class AuthProvider {
    static async Login(login: string, password: string): Promise<Result<LoginResultDTO | null>> {
        const data = {login, password}        
        const response = await axios.post(`Auth/LoginEmployee`, data)
        if(!response.data.isSuccess) return Result.fail(response.data.errors[0])

        const authResult: LoginResultDTO = {
            employeeId: response.data.value.employeeId,
            token: response.data.value.token,
        }
        return Result.success(authResult)
    }
}