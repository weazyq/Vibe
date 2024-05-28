import { Visibility, VisibilityOff } from "@mui/icons-material"
import { Button, FormControl, Grid, IconButton, InputAdornment, InputLabel, OutlinedInput, Stack, TextField, Typography } from "@mui/material"
import { useEffect, useState } from "react"
import { useAuthContext } from "../../domain/infrastructure/authContext"
import { useNavigate } from "react-router-dom"
import { AuthProvider } from "../../domain/infrastructure/authProvider"

interface AuthData{
  login: string,
  password: string,
}

function Auth() {
  const defaultValue: AuthData = {
    login: '',
    password: ''
  }

  const [authData, setAuthData] = useState<AuthData>(defaultValue)
  const [isShowPassword, setIsShowPassword] = useState<boolean>(false)
  const {isAuthenticated, onAuthorize} = useAuthContext()

  const navigate = useNavigate()

  useEffect(() => {
    if(isAuthenticated) return navigate('/')
  }, [])

  const handleClickShowPassword = () => setIsShowPassword((show) => !show);

  const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
  };

  async function handleAuthClick(){
    auth(authData.login, authData.password)
  }

  function handleChangeLogin(login: string){
    setAuthData((authData) => ({...authData, login}))
  }

  function handleChangePassword(password: string){
    setAuthData((authData) => ({...authData, password}))
  }

  return (
    <Grid container spacing={2} height={'100%'} alignItems={'center'}>
      <Grid item xs={12} md={6} textAlign={'center'}>
        <Typography variant="h3">Авторизация в Vibe</Typography>
      </Grid>
      <Grid item xs={12} lg={4}>
        <Stack spacing={2}>
          <TextField 
            label="Логин" 
            fullWidth
            value={authData.login}
            onChange={(e) => handleChangeLogin(e.target.value)}
          />
          <FormControl fullWidth variant="outlined">
            <InputLabel htmlFor="outlined-adornment-password">Пароль</InputLabel>
            <OutlinedInput
              id="outlined-adornment-password"
              type={isShowPassword ? 'text' : 'password'}
              endAdornment={
                <InputAdornment position="end">
                  <IconButton
                    aria-label="toggle password visibility"
                    onClick={handleClickShowPassword}
                    onMouseDown={handleMouseDownPassword}
                    edge="end"
                    >
                    {isShowPassword ? <VisibilityOff /> : <Visibility />}
                  </IconButton>
                </InputAdornment>
              }
              label="Password"
              value={authData.password}
              onChange={(e) => handleChangePassword(e.target.value)}
            />
          </FormControl>         
          <Button 
            variant="contained" 
            fullWidth 
            onClick={handleAuthClick}>
            Авторизоваться
          </Button>
        </Stack>
      </Grid>
    </Grid>
  )

  async function auth(login: string, password: string) {
    const response = await AuthProvider.Login(login, password)
    if(!response.isSuccess || response.data == null) return
  
    onAuthorize(response.data.employeeId, response.data.token)
    return navigate('/')
  }
}

export default Auth