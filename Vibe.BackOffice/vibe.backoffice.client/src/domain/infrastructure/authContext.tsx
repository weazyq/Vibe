import { PropsWithChildren, createContext, useContext, useState } from "react"

interface IAuthContext {
    isAuthenticated: boolean,
    onAuthorize: (employeeId: string, token: string) => void
    checkAuthorize: () => void
    logout: () => void
}

const defaultValue: IAuthContext = {
    isAuthenticated: false,
    onAuthorize: () => {},
    checkAuthorize: () => {},
    logout: () => {}
}

export const AuthContext = createContext<IAuthContext>(defaultValue)

function AuthProvider(props: PropsWithChildren<{}>) {
    function getDefaultValue(): IAuthContext {
        const isAutenticated = checkAuthorize()

        return {
            isAuthenticated: isAutenticated,
            onAuthorize: authorize,
            checkAuthorize: checkAuthorize,
            logout: logout,
        }
    }
    
    const [authContext, setAuthContext] = useState<IAuthContext>(getDefaultValue())

    function changeContext(context: Partial<IAuthContext>) {
        setAuthContext((prevContext) => ({ ...prevContext, ...context }))
    }

    function authorize(employeeId: string, token: string) {
        localStorage.setItem('employeeId', employeeId)
        localStorage.setItem('token', token)

        changeContext({isAuthenticated: true})
    }

    function checkAuthorize(): boolean {
        const token = localStorage.getItem('token')
        if(token == null) return false

        return true
    }

    function logout() {
        localStorage.clear()
        changeContext({
            isAuthenticated: false
        })
    }

    return(
        <AuthContext.Provider value={authContext}>
            {props.children}
        </AuthContext.Provider>
    )
}

export function useAuthContext() {
    const context = useContext(AuthContext)
    return context
}

export default AuthProvider