import React, { createContext, useState, ReactNode, useContext, useEffect } from "react";
import { auth } from "../src/firebaseConfig";
import { onAuthStateChanged, User } from "firebase/auth";

// Defina os tipos para o contexto
interface AuthContextType {
  user: User | null;
  login: (token: string) => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);

  useEffect(() => {
    const unsubscribe = onAuthStateChanged(auth, (currentUser) => {
      setUser(currentUser);
    });

    // Limpeza ao desmontar o componente
    return () => unsubscribe();
  }, []);

  const login = (token: string) => {
    // Você pode armazenar o token ou usar conforme necessário
    console.log("Login realizado com token:", token);
  };

  const logout = () => {
    // Função de logout, pode ser expandida conforme necessidade
    console.log("Logout realizado");
  };

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

// Hook personalizado para usar o contexto
export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
