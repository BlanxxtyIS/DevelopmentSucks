import "./Header.css";
import { Link, useNavigate } from "react-router-dom";

export default function Header({user, setUser}) {
    const navigate = useNavigate();

const handleLogout = async () => {

    try {
        await fetch("https://localhost/api/auth/logout", {  
            method: "POST",
            credentials: "include"
        });
        localStorage.removeItem("accessToken");
        setUser(null);
        navigate("/login");
    } catch (err) {
        console.error("Ошибка при выходе:", err);
    }
};

    return (
        <header className="app-header">
            <div className="logo">
                <Link to="/">🏠 Home</Link>
            </div>
             <div>Development Sucks</div>
            <div className="auth">
                {user 
                    ? <>
                        <span>Привет, {user ?? "пользователь"}</span>
                        <button onClick={handleLogout}>Выйти</button>                
                    </>
                : <>
                <Link to="/login">
                    <button >Войти</button>
                </Link>
                
                <Link to="/register">
                    <button >Зарегистрироваться</button>
                </Link>
                </>
                }   
            </div>
        </header>
    );
}