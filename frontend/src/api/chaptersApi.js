import { AuthFetch } from "../pages/components/AuthFetch";

//const BASE_URL = 'http://localhost:8080/api/chapters';
const BASE_URL = 'https://localhost/api/chapters';

export async function  getAllChapters(token) {
    const response = await fetch(BASE_URL, {
        headers: {
            'Authorization': `Bearer ${token}`
        },
        credentials: 'include'
    });

    if (!response.ok) {
        throw new Error('Ошибка при загрузке глав');
    }

    return response.json();
}

export async function  addChapter(chapter) {
    const response = await AuthFetch(BASE_URL, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(chapter),
        credentials: 'include'
    });

    if (!response.ok) {
        throw new Error('Ошибка при добавлении главы');
    }

    return response.json();
}

export async function editChapter(chapter) {
    const response = await AuthFetch(BASE_URL, {
        method: 'PUT',
        body: JSON.stringify(chapter),
        headers: {
            'Content-Type': 'application/json'
        },
        credentials: 'include'
    });

    if (!response.ok) {
        throw new Error('Ошибка при редактировании курса');
    }

    return;
}

export async function deleteChapter(id) {
    const response = await AuthFetch(`${BASE_URL}/${id}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        },
        credentials: 'include'
    });

    if (!response.ok) {
        throw new Error('Ошибка при удалении курса');
    }

    return;
}