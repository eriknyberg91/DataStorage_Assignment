import { useState } from "react";
import ProjectComponent from "./Components/ProjectComponent";
import ProjectList from "./Components/ProjectList";

function App() {
    const [refresh, setRefresh] = useState(false);

    const handleProjectCreated = () => {
        setRefresh((prev) => !prev);
    };

    return (
        <>
            <ProjectComponent onProjectCreated={handleProjectCreated} />
            <ProjectList refresh={refresh} />
        </>
    );
}

export default App;