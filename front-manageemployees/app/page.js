import AddDepartement from "./components/departments/AddDepartement";
import DepartmentList from "./components/departments/DepartmentList";
import { DepartmentProvider } from "./contexts/DepartmentContext";
import { ToastContainer } from 'react-toastify';

const Home = async () => {
  return (
    <main className='max-w-4xl mx-auto mt-4'>
      <DepartmentProvider>
        <div className='text-center my-5 flex flex-col gap-4'>
          <h1 className='text-2xl font-bold'>Management d'employés</h1>
          <AddDepartement />
        </div>
        <DepartmentList />
      </DepartmentProvider>
      <ToastContainer />
    </main>
  );
}

export default Home
