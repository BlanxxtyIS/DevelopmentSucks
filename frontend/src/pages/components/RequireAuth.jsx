import React from "react";
import { Navigate } from "react-router-dom";
import Header from "./Header/Header.jsx";
import Layout from "./Layout/Layout.jsx";

const RequireAuth = ({ children }) => {
    const token = localStorage.getItem("accessToken");

    if (!token) {
        return (
            <>
            <Layout />
            <div style={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                justifyContent: 'center',
                height: '70vh',
                fontSize: '1.2rem'
            }}>
                <p>⚠️ Пожалуйста, авторизуйтесь для доступа к этой странице.</p>
                <a href="/login" style={{ marginTop: '20px', fontSize: '1rem', textDecoration: 'underline' }}>
                    Перейти к авторизации
                </a>
            </div>
            </>
        );
    }

    return children;
};

export default RequireAuth;
