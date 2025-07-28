export async function  AuthFetch(input, init = {}) {
    const accessToken = localStorage.getItem('accessToken');

    init.headers = {
        ...init.headers,
        Authorization: `Bearer ${accessToken}`,
    };

    init.credentials = 'include';

    let response = await fetch(input, init);

    if (response.status === 401) {
        const refreshed = await tryRefreshToken();
        if (refreshed) {
            // Пробуем снова — уже с новым токеном
            init.headers.Authorization = `Bearer ${refreshed.accessToken}`;
            response = await fetch(input, init);
        }
    }
    return response;
}

async function tryRefreshToken() {
    try {
        const res = await fetch('https://localhost/api/auth/refresh', {
            method: 'POST',
            credentials: 'include',
        });

        if (!res.ok) throw new Error('Не удалось обновить токен');

        const data = await res.json();
        localStorage.setItem('accessToken', data.accessToken);
        return data;
    } catch (error) {
        console.error('Ошибка обновления токена:', error);
        // Тут можно сделать logout, редирект и т.п.
        return null;
    }
}