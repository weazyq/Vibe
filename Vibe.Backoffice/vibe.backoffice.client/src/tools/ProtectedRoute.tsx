import { Navigate } from "react-router-dom"
import { useAuthContext } from "../domain/infrastructure/authContext"

function ProtectedRoute({ children }: any) {
  const {isAuthenticated} = useAuthContext()
    
  if(!isAuthenticated){
    return <Navigate to="/auth"/>
  }

  return children
}

export default ProtectedRoute