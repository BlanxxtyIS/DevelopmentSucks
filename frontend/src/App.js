import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import HomePage from './pages/HomePage';
import AdminPage from './pages/AdminPage';
import CurrentLessonPage from './pages/components/CurrentLessonPage';
import RequireAdmin from './pages/components/RequireAdmin';
import Layout from './pages/components/Layout/Layout';
import './styles/styles.css';

function App() {  
  return (
    <Router>
      <Routes>
        {/* Без Layout */}
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />

        {/* С Layout */}
        <Route path='/' element={<Layout>
          <HomePage />
        </Layout>} />

        <Route path='/admin' element={
          <RequireAdmin>
            <Layout>
              <AdminPage />
            </Layout>
          </RequireAdmin>
        } />

        <Route path='/lessons/:order?' element={<Layout>
          <CurrentLessonPage />
        </Layout>} />
      </Routes>
    </Router>
  );
}

export default App;