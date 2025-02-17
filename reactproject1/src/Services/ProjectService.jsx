//All Frontend is created with the help of ChatGPT

const BASE_URL = "https://localhost:7225/api/project";
const API_URL = "https://localhost:7225/api"; // Base API for lookup data

export const createProject = async (projectForm) => {
    const response = await fetch(BASE_URL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(projectForm),
    });

    if (!response.ok) {
        const errorMessage = await response.text();
        throw new Error(errorMessage || "Failed to create project");
    }

    return response.json();
};

// Fetch existing customers, statuses, users, products
export const getCustomers = async () => {
    const response = await fetch(`${API_URL}/customer`);
    return response.json();
};

export const getStatuses = async () => {
    const response = await fetch(`${API_URL}/statustype`);
    return response.json();
};

export const getUsers = async () => {
    const response = await fetch(`${API_URL}/user`);
    return response.json();
};

export const getProducts = async () => {
    const response = await fetch(`${API_URL}/product`);
    return response.json();
};

export const getProjects = async () => {
    const response = await fetch(`${API_URL}/project`);
    return response.json();
};

export const deleteProject = async (id) => {
    const response = await fetch(`${API_URL}/project/${id}`, { method: "DELETE" });
    if (!response.ok) throw new Error("Failed to delete project");
};



