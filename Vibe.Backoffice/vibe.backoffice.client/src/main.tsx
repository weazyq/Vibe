import React from 'react'
import ReactDOM from 'react-dom/client'
import AuthProvider from './domain/infrastructure/authContext.tsx'
import Layout from './sharedComponents/layout.tsx'
import { Route, BrowserRouter as Router, Routes } from 'react-router-dom'
import Home from './pages/home/home.tsx'
import ProtectedRoute from './tools/ProtectedRoute.tsx'
import Auth from './pages/infrastructure/auth.tsx'

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
              </Routes>
            </Layout>
          }/>
        </Routes>
    </Router>
  </AuthProvider>
)