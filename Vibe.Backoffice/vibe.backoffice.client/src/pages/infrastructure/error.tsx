import { Box, Button, Stack, Typography } from "@mui/material"
import { useNavigate } from "react-router-dom"

function Error() {
    const navigate = useNavigate()

    function handleClickGoHome(){
        navigate('/')
    }

  return (
    <Box height={"100%"} sx={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
        <Stack spacing={2}>
            <Typography variant="h1" align="center">404</Typography>
            <Typography variant="h4" align="center">Упс... Страница не существует</Typography>
            <Button variant="contained" onClick={handleClickGoHome}>Домой</Button>      
        </Stack>
    </Box>
  )
}

export default Error