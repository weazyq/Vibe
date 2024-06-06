import axios from "axios";
import { SupportRequest, mapToSupportRequests } from "./supportRequest";
import { SupportRequestDetail, mapToSupportRequestDetail } from "./supportRequestDetail";
import Result from "../../tools/result";
import { SupportMessageDTO } from "./supportMessageDTO";

export class SupportRequestProvider{
    
    static async saveSupportMessage(message: SupportMessageDTO): Promise<Result<null>> {
        const token = await localStorage.getItem('token')
        
        const response = await axios.post('SupportRequests/SaveSupportMessage', message, 
        {
          headers: {
            Authorization: `Bearer ${token}`
          }
        })
    
        if(!response.data.isSuccess) return Result.fail(response.data.errors[0])
        return Result.success(null)
    }

    static async list(): Promise<SupportRequest[]> {
        const token = localStorage.getItem('token')

        const response = await axios.get('/SupportRequests/List', {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })

        return mapToSupportRequests(response.data)
    }

    static async getSupportRequestDetail(id: string): Promise<SupportRequestDetail | null>{
        const token = localStorage.getItem('token')

        const response = await axios.get('/SupportRequests/GetSupportRequestDetail', {
            params: {
                id
            },
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        if(response.data == null) return null

        return mapToSupportRequestDetail(response.data)
    }
}