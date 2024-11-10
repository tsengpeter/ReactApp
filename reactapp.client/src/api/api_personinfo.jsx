const BASE_URL = '/api/Personinfo';

export const getPersoninfo = async (query) => {
    // 將查詢參數轉換為查詢字串格式
    const queryParams = new URLSearchParams(query).toString();
    const response = await fetch(`${BASE_URL}/GetPersoninfo?${queryParams}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
    });
    // 返回解析後的 JSON 資料
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
    // 返回解析後的 JSON 資料
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
    // 返回解析後的 JSON 資料
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
    // 返回解析後的 JSON 資料
    return response.json();
};
