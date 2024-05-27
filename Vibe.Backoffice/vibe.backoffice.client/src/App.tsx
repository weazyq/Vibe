import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import './App.css';
import ProtectedRoute from './tools/ProtectedRoute';
import Home from './pages/home';
import Auth from './pages/auth';

function App() {
    const router = createBrowserRouter([
        {
            path: "/",
            element: <ProtectedRoute>
                <Home/>
            </ProtectedRoute>,
            errorElement: <>Ошибка</>,
        },
        {
            path: "/auth",
            element: <Auth/>,
        },
    ])

    return (
        <RouterProvider router={router}/>
    )
}

export default App;