import React from "react";
import "./Header.css";
import { Link, useNavigate } from "react-router-dom";

export default function Header({user, setUser}) {
    const navigate = useNavigate();

const handleLogout = async () => {

    try {
        const response = await fetch("https://localhost/api/auth/logout", {  
            method: "POST",
            credentials: "include"
        });
        console.log("НАЖАЛИ");
        localStorage.removeItem("accessToken");
        setUser(null);
        console.log("НАЖАЛИ");
        navigate("/login");
    } catch (err) {
        console.log("НАЖАЛИ");
        console.error("Ошибка при выходе:", err);
    }
};

    return (
        <header className="app-header">
            <div className="logo">
                <Link to="/">🏠 Home</Link>
            </div>
            <div className="auth">
                {user 
                    ? <>
                        <span>Привет, {user?.username ?? "пользователь"}</span>
                        <button onClick={handleLogout}>Logout</button>                
                    </>
                : <>
                <Link to="/login">
                    <button >Login</button>
                </Link>
                
                <Link to="/register">
                    <button >Register</button>
                </Link>
                </>
                }   
            </div>
        </header>
    );
}