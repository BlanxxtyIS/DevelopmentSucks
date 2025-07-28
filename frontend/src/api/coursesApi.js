import { AuthFetch } from "../pages/components/AuthFetch";

//const BASE_URL = 'http://localhost:8080/api/courses';
const BASE_URL = 'https://localhost/api/courses';

export async function getAllCourses() {
    const response = await AuthFetch(BASE_URL);

    if (!response.ok) {
        throw new Error('Ошибка при загрузке курсов');
    } 

    return response.json();
}

export async function addCourse(course) {
    const response = await AuthFetch(BASE_URL, {
        method: 'POST',
        body: JSON.stringify(course),
        headers: {
            'Content-Type': 'application/json'
        },
    });

    if (!response.ok) {
        throw new Error('Ошибка при добавлении курса');
    }

    return response.json();
}

export async function editCourse(course) {
    const response = await AuthFetch(BASE_URL, {
        method: 'PUT',
        body: JSON.stringify(course),
        headers: {
            'Content-Type': 'application/json'
        },
    });

    if (!response.ok) {
        throw new Error('Ошибка при редактировании курса');
    }
    return;
}

export async function deleteCourse(id) {
    const response = await AuthFetch(`${BASE_URL}/${id}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        },
    });

    if (!response.ok) {
        throw new Error('Ошибка при удалении курса');
    }

    return;
}