let BE_ENPOINT = "";

const HEADERS = {
    "Content-Type": "application/json",
    accept: "application/json",
};
const fetchGet = async (uri, onSuccess, onFail) => {
    try {
        const res = await fetch(BE_ENPOINT + uri, {
            method: "GET",
            headers: HEADERS,
        });
        if (!res.ok) {
            onFail();
            return;
        }
        const data = await res.json();
        onSuccess(data);
    } catch {
        onFail();
    }
};
const fetchPost = async (uri, reqData, onSuccess, onFail) => {
    try {
        const res = await fetch(BE_ENPOINT + uri, {
            method: "POST",
            headers: HEADERS,
            body: JSON.stringify(reqData),
        });

        if (!res.ok) {
            onFail();
            return;
        }
        const data = await res.json();
        onSuccess(data);
    } catch {
        onFail();
    }
};

const fetchDelete = async (uri, reqData, onSuccess, onFail) => {
    try {
        const res = await fetch(BE_ENPOINT + uri, {
            method: "DELETE",
            headers: HEADERS,
            body: JSON.stringify(reqData),
        });

        if (!res.ok) {
            onFail();
            return;
        }
        const data = await res.json();
        onSuccess(data);
    } catch {
        onFail();
    }
};

const fetchPut = async (uri, reqData, onSuccess, onFail) => {
    try {
        const res = await fetch(BE_ENPOINT + uri, {
            method: "PUT",
            headers: HEADERS,
            body: JSON.stringify(reqData),
        });

        if (!res.ok) {
            onFail();
            return;
        }
        const data = await res.json();
        onSuccess(data);
    } catch {
        onFail();
    }
};
