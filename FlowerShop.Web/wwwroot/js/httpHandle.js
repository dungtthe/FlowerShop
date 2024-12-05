let BE_ENPOINT = "";

const HEADERS = {
  "Content-Type": "application/json",
  accept: "application/json",
};
const fetchGet = async (uri, onSuccess, onFail, onException) => {
  try {
    const res = await fetch(BE_ENPOINT + uri, {
      method: "GET",
      headers: HEADERS,
    });
    if (res.status === 401) {
      window.location.href = `/login`;
      return;
    }
    if (res.status === 404) {
      window.location.href = `/error/notfound`;
      return;
    }

      const data = await res.json();
      if ("redirect" in data) {
          const urlNew = data.redirect;
          window.location.href = urlNew;
          return;
      }
    if (!res.ok) {
      onFail(data);
      return;
    }
    onSuccess(data);
  } catch {
    onException();
  }
};

const fetchPost = async (uri, reqData, onSuccess, onFail, onException) => {
  try {
    const res = await fetch(BE_ENPOINT + uri, {
      method: "POST",
      headers: HEADERS,
      body: JSON.stringify(reqData),
    });
    if (res.status === 401) {
      window.location.href = `/login`;
      return;
    }
    if (res.status === 404) {
      window.location.href = `/error/notfound`;
      return;
    }
    const data = await res.json();
    if (!res.ok) {
      onFail(data);
      return;
    }
    onSuccess(data);
  } catch {
    onException();
  }
};

const fetchDelete = async (uri, reqData, onSuccess, onFail, onException) => {
  try {
    const res = await fetch(BE_ENPOINT + uri, {
      method: "DELETE",
      headers: HEADERS,
      body: JSON.stringify(reqData),
    });
    if (res.status === 401) {
      window.location.href = `/login`;
      return;
    }
    if (res.status === 404) {
      window.location.href = `/error/notfound`;
      return;
    }
    const data = await res.json();
    if (!res.ok) {
      onFail(data);
      return;
    }
    onSuccess(data);
  } catch {
    onException();
  }
};

const fetchPut = async (uri, reqData, onSuccess, onFail, onException) => {
  try {
    const res = await fetch(BE_ENPOINT + uri, {
      method: "PUT",
      headers: HEADERS,
      body: JSON.stringify(reqData),
    });
    if (res.status === 401) {
      window.location.href = `/login`;
      return;
    }
    if (res.status === 404) {
      window.location.href = `/error/notfound`;
      return;
    }
    const data = await res.json();
    if (!res.ok) {
      onFail(data);
      return;
    }
    onSuccess(data);
  } catch {
    onException();
  }
};
