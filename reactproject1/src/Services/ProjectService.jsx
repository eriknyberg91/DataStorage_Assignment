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

export const getProjectById = async (id) => {
    try {
        const response = await fetch(`${API_URL}/project/${id}`);

        if (!response.ok) {
            throw new Error("Failed to get project");
        }

        const data = await response.json();
        return data; 
    } catch (error) {
        console.error("Error fetching project:", error);
        return null;
    }
};

export const updateProject = async (id, projectData) => {
    const response = await fetch(`${API_URL}/project/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(projectData),
    });

    if (!response.ok) {
        throw new Error("Failed to update project");
    }

    return await response.json();
};






