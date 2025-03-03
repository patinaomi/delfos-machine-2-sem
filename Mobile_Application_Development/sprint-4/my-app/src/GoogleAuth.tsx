import React from "react";
import { Button, Alert } from "react-native";
import * as WebBrowser from "expo-web-browser";
import * as Google from "expo-auth-session/providers/google";
import { getAuth, signInWithCredential, GoogleAuthProvider } from "firebase/auth";
import { auth } from "../src/firebaseConfig"; 

WebBrowser.maybeCompleteAuthSession();

const GoogleAuth: React.FC<{ navigation: any }> = ({ navigation }) => {
  const [request, response, promptAsync] = Google.useIdTokenAuthRequest({
    clientId: "555072563253-l3puamm18l0h7cns2cctgrn37k9o2nrb.apps.googleusercontent.com",
  });

  React.useEffect(() => {
    if (response?.type === "success") {
      const { id_token } = response.params;
      const credential = GoogleAuthProvider.credential(id_token);
      signInWithCredential(auth, credential)
        .then(() => {
          Alert.alert("Sucesso", "Login com Google realizado!");
          navigation.navigate("SessaoRestrita"); // 🔹 Agora redireciona para a tela restrita!
        })
        .catch((error) => {
          Alert.alert("Erro", "Falha ao autenticar com Google.");
          console.error(error);
        });
    }
  }, [response]);

  return (
    <Button
      title="Entrar com Google"
      disabled={!request}
      onPress={() => promptAsync()}
    />
  );
};

export default GoogleAuth;
