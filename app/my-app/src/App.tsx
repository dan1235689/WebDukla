import { Routes, Route } from "react-router-dom";
import { Teams } from "./components";

export default function App() {
  return (
      <Routes>
        <Route path='/' element={Teams({})}>
          <Route path='index.html' element={Teams({})} />
        </Route>
      </Routes>
  );
}

