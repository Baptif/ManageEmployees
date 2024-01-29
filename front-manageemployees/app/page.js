import { ToastContainer } from 'react-toastify';
import Link from "next/link";

const Home = async () => {
  return (
    <main className='max-w-4xl mx-auto mt-4'>
        <div className='text-center my-5 flex flex-col gap-4'>
          <h1 className='text-2xl font-bold'>Management d'employés</h1>
        </div>
        <Link href="/department">
          <p>Départements</p>
        </Link>
        <Link href="/employee">
          <p>Employés</p>
        </Link>
    </main>
  );
}

export default Home
