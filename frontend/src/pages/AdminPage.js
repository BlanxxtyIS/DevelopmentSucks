import RefreshToken from './components/AdminPanel/RefreshToken';
import LessonPage from './components/AdminPanel/LessonPage';
import CoursesPage from './components/AdminPanel/CoursesPage';
import ChapterPage from './components/AdminPanel/ChapterPage';

export default function AdminPage() {
    return (
        <div>
            <h1>Панель администратора</h1>
            <hr/>
            <CoursesPage />
            <hr />
            <ChapterPage />
            <hr />
            <LessonPage />
        </div>
    )
}