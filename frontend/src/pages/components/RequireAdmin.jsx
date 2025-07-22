import React from "react";
import { jwtDecode } from 'jwt-decode';
import { Navigate, useNavigate } from 'react-router-dom';


const RequireAdmin = ({children}) => {
    const token = localStorage.getItem('accessToken');
    const navigate = useNavigate();

    if (!token) {
        return <Navigate to="/login" replace />
    }

    try {
        const decoded = jwtDecode(token);
        const role = decoded.role || decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

        if (role !== 'Admin') {
            return (
            <div style={{
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                    justifyContent: 'center',
                    height: '70vh',
                    fontSize: '1.2rem'
                }}>
                    <p>❌ У вас недостаточно прав для доступа к этой странице.</p>
                    <button
                        style={{
                            marginTop: '20px',
                            padding: '10px 20px',
                            fontSize: '1rem',
                            cursor: 'pointer'
                        }}
                        onClick={() => navigate('/')}
                    >
                        На главную
                    </button>
                </div>
            )
        }
    } catch (e) {
        return <Navigate to="/login" replace />
    }

    return children;
};

export default RequireAdmin;