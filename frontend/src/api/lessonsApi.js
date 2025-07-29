import { AuthFetch } from "../pages/components/AuthFetch";

//const BASE_URL = 'http://localhost:8080/api/lessons';
const BASE_URL = 'https://localhost/api/lessons';

export async function getAllLessons() {
    const response = await AuthFetch(BASE_URL);

    if (!response.ok) {
        throw new Error('Ошибка при загрузке уроков');
    }
    return response.json();
}

export async function addLesson(lesson) {
    const response = await AuthFetch(BASE_URL, {
        method: 'POST',
        body: JSON.stringify(lesson),
        headers: {
            'Content-Type': 'application/json',
        },  
    });

    if (!response.ok) {
        throw new Error('Ошибка при добавлении урока');
    }
    
    return response.json();
}

export async function editLesson(lesson) {
    const response = await AuthFetch(BASE_URL, {
        method: 'PUT',
        body: JSON.stringify(lesson),
        headers: {'Content-Type': 'application/json'},
    });

    if (!response.ok) {
        console.log(response.status);
        throw new Error('Ошибка при редактировании урока');
    }
    return;
}

export async function deleteLesson(id) {
    const response = await AuthFetch(`${BASE_URL}/${id}`, {
        method: 'DELETE',
        headers: {'Content-Type': 'application/json'},
    });

    if (!response.ok) {
        console.log(response.status);
        throw new Error('Ошибка при удалении урока');
    }
    return;
}