const BASE_URL = '/api/Personinfo';

export const getPersoninfo = async (query) => {
    // �N�d�߰Ѽ��ഫ���d�ߦr��榡
    const queryParams = new URLSearchParams(query).toString();
    const response = await fetch(`${BASE_URL}/GetPersoninfo?${queryParams}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
    });
    // ��^�ѪR�᪺ JSON ���
    return response.json();
};

export const addPersoninfo = async (data) => {
    const response = await fetch(`${BASE_URL}/AddNewPersoninfo`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    });
    // ��^�ѪR�᪺ JSON ���
    return response.json();
};

export const deletePersoninfo = async (data) => {
    const response = await fetch(`${BASE_URL}/DeletePersoninfo`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    });
    // ��^�ѪR�᪺ JSON ���
    return response.json();
};

export const updatePersoninfo = async (data) => {
    const response = await fetch(`${BASE_URL}/UpdatePersoninfo`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    });
    // ��^�ѪR�᪺ JSON ���
    return response.json();
};
