import { Box, Button, Input, Stack, SxProps, Theme, Typography } from "@mui/material"
import { Send } from '@mui/icons-material';
import { AccountCircle, Comment, CommentsDisabled } from "@mui/icons-material"
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr"
import { SupportMessage, mapToSupportMessage } from "../../domain/techSupports/supportMessage"
import { useEffect, useState } from "react"
import { SupportRequestProvider } from "../../domain/techSupports/supportRequestProvider"
import { SupportRequestDetail } from "../../domain/techSupports/supportRequestDetail"
import { SupportMessageDTO } from "../../domain/techSupports/supportMessageDTO"

interface SupportRequestChatProps {
    supportRequestId: string | null
}

function SupportRequestChat({supportRequestId}: SupportRequestChatProps) {
    const [, setConnection] = useState<HubConnection | null>(null)
    const [supportRequest, setSupportRequest] = useState<SupportRequestDetail | null>(null)
    const [messages, setMessages] = useState<SupportMessage[]>([])
    const [message, setMessage] = useState<string>('')

    useEffect(() => {
        if(supportRequestId == null) return
        loadSupportRequestDetail(supportRequestId)
    }, [supportRequestId])

    useEffect(() => {
        if(supportRequest == null) return
        joinChat()
    }, [supportRequest])

    async function loadSupportRequestDetail(supportRequestId: string) {
        const supportRequest: SupportRequestDetail | null = await getSupportRequestDetail(supportRequestId)
        setSupportRequest(supportRequest)
        setMessages(supportRequest?.messages ?? [])
    }

    async function handleSaveMessage(){
        const messageDTO = new SupportMessageDTO(message, supportRequest!.id)
        const response = await SupportRequestProvider.saveSupportMessage(messageDTO)
        if(!response.isSuccess) return 

        setMessage('')
    }

    const joinChat = async() => {
        if(supportRequest == null) return

        const connection = new HubConnectionBuilder()
            .withUrl(`http://localhost:7221/SupportRequestChat`)
            .withAutomaticReconnect()
            .build();

        const userId = await localStorage.getItem('employeeId')
            
        connection.on("ReceiveMessage", (message: SupportMessage) => {
            const mappedMessage = mapToSupportMessage(message)
            setMessages((messages) => [...messages, mappedMessage])
        })

        try {
            await connection.start();
            await connection.invoke("JoinChat", {supportRequestId: supportRequest.id, userId})
            setConnection(connection)
        } catch (error) {
            console.log(error)
        }
    }

    return (
        <Box sx={{border: 1, borderColor: 'action.disabled', borderRadius: 2, height: '100%'}}>
            {supportRequest == null 
                ? <Box display={'flex'} justifyContent={'center'} alignItems={'center'} height={'100%'}>
                    <Stack alignItems={'center'} spacing={3}>
                        <CommentsDisabled sx={{fontSize: 128}} color='disabled'/>
                        <Typography variant="h3">Не выбран чат</Typography>
                        <Typography variant="h6">Выберите активный запрос слева</Typography>
                    </Stack>
                </Box>
                : <Box height={'100%'} display={'flex'} flexDirection={'column'}>
                    <Box sx={{borderBottom: 1, borderBottomColor: 'action.disabled'}}>
                        <Typography align="center">Обращение: {supportRequest.title}</Typography>
                        <Typography>
                            <AccountCircle/> 
                            {supportRequest.client.name} ({supportRequest.client.phone})
                            </Typography>
                        <Typography>
                            <Comment/>{supportRequest.description}
                        </Typography>
                    </Box>
                    <Box flex={1}>
                        {messages.map(message => renderMessage(message))}
                    </Box>
                    <Box>
                        <Input 
                            autoFocus
                            fullWidth
                            placeholder="Введите сообщение"
                            endAdornment={<Button onClick={handleSaveMessage}><Send/></Button>}
                            onChange={(e) => setMessage(e.target.value)}
                            value={message}
                            />
                    </Box>
                </Box>
            }
        </Box>
    )

    function renderMessage(message: SupportMessage){
        const isClientMessage = supportRequest!.client.id === message.createdBy
        
        const baseMessageStyle = {
            display: 'flex',
            position: 'relative',
            maxWidth: '50%',
            border: 1,
            borderColor: 'action.disabled',
            borderRadius: '0 10px 10px 0',
            marginTop: 1,
            padding: 2,
        }

        const messageStyle: SxProps<Theme> = isClientMessage 
            ? {
                justifyContent: 'flex-start',
            }
            : {
                color: 'white',
                backgroundColor: 'primary.light',
            }

        return (
            <Box key={message.id} sx={[baseMessageStyle, messageStyle]}>
                <Typography>{message.text}</Typography>
                <Box style={{position: 'absolute', bottom: 2, right: 5}}>
                    <Typography>{message.createdAt.toLocaleTimeString()}</Typography>
                </Box>
            </Box>
        )
    }
}

async function getSupportRequestDetail(supportRequestId: string): Promise<SupportRequestDetail | null>{
    return SupportRequestProvider.getSupportRequestDetail(supportRequestId)
}

export default SupportRequestChat