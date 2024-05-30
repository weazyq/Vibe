import ReactDOM from 'react-dom/client'
import AuthProvider from './domain/infrastructure/authContext.tsx'
import Layout from './sharedComponents/layout.tsx'
import { Route, BrowserRouter as Router, Routes } from 'react-router-dom'
import Home from './pages/home/home.tsx'
import ScootersList from './pages/scooters/scootersList.tsx'
import EmployeesList from './pages/employees/employeeList.tsx'
import ProtectedRoute from './tools/ProtectedRoute.tsx'
import Auth from './pages/infrastructure/auth.tsx'
import SupportRequestsPage from './pages/supportRequests/supportRequestsPage.tsx'

ReactDOM.createRoot(document.getElementById('root')!).render(
  <AuthProvider>
    <Router>
        <Routes>
          <Route path='/auth' element={<Auth/>}/>
          <Route path='*' element={
            <Layout>
              <Routes>
                <Route path='/' element={<ProtectedRoute>
                  <Home/>
                </ProtectedRoute>}/>
                <Route path='/scooters' element={<ProtectedRoute>
                  <ScootersList/>
                </ProtectedRoute>}/>
                <Route path='/employees' element={<ProtectedRoute>
                  <EmployeesList/>
                </ProtectedRoute>}/>
                <Route path='/supportRequests' element={<ProtectedRoute>
                  <SupportRequestsPage/>
                </ProtectedRoute>}/>
              </Routes>
            </Layout>
          }/>
        </Routes>
    </Router>
  </AuthProvider>
)