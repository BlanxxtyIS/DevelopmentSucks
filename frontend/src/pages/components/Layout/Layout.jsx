import React, { useEffect, useState } from "react";
import Header from "../Header/Header";
import Sidebar from "../Slidebar/Slidebar";
import * as lessonsApi from "../../../api/lessonsApi";
import "./Layout.css";
import { useNavigate } from "react-router-dom";

export default function Layout({ children }) {
  const [lessons, setLessons] = useState([]);
  const [user, setUser] = useState(null);
  const token = localStorage.getItem('accessToken');

    useEffect(() => {
    async function fetchUser() {
      try {
        const res = await fetch("https://localhost/api/auth/me", {
          method: 'GET',
          headers: {
            'Authorization': `Bearer ${token}`,
          },
          credentials: "include",
        });
        if (res.ok) {
          const data = await res.json();
          setUser(data.username); // предполагается, что API возвращает { username: "..." }
        } else {
          setUser(null);
        }
      } catch {
        setUser(null);
      }
    }
    fetchUser();
  }, []);

  useEffect(() => {
    async function loadLessons() {
      try {
        const data = await lessonsApi.getAllLessons(token);
        setLessons(data);
      } catch (err) {
        console.log('Ошибка при загрузке');
      }
  }
  loadLessons();
  }, []);


  return (
    <>
      <Header user={user} setUser={setUser}/>
      <Sidebar lessons={lessons} />
      <main className="layout-content">
        {children}
      </main>
    </>
  );
}
