import { FirebaseApp, initializeApp } from "firebase/app";
import { getFirestore } from "firebase/firestore";
import { getAuth } from "firebase/auth";
import ReactNativeAsyncStorage from "@react-native-async-storage/async-storage";


const firebaseConfig = {
  apiKey: "AIzaSyA1tKBWqM7hzs5sg_Ks3HYyLj8GrVknvwg",
  authDomain: "mobileproject-8d8ae.firebaseapp.com",
  projectId: "mobileproject-8d8ae",
  storageBucket: "mobileproject-8d8ae.firebasestorage.app",
  messagingSenderId: "555072563253",
  appId: "1:555072563253:web:63a606b71115d7e1cb297c"
};

// Inicializa o Firebase
console.log("Inicializando Firebase...");
const app = initializeApp(firebaseConfig);
export const db = getFirestore(app); 
export const auth = getAuth(app);
export default app;