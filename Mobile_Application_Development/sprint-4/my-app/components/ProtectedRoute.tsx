import React, { useContext } from "react";
import { ActivityIndicator, View } from "react-native";
import { AuthContext } from "../contexts/AuthProvider";
import LoginScreen from "../screens/LoginScreen";

interface ProtectedRouteProps {
  children: React.ReactNode;
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ children }) => {
  const { userToken, isLoading } = useContext(AuthContext);

  if (isLoading) {
    return (
      <View style={{ flex: 1, justifyContent: "center", alignItems: "center" }}>
        <ActivityIndicator size="large" />
      </View>
    );
  }

  if (!userToken) {
    return <LoginScreen navigation={undefined} />;
  }

  return <>{children}</>;
};

export default ProtectedRoute;