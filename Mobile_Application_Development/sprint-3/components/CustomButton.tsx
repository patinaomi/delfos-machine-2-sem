import React from "react";
import { TouchableOpacity, Text, StyleSheet, DimensionValue, View, ViewStyle, TextStyle } from "react-native";

type CustomButtonProps = {
  title: string;
  onPress: () => void;
  backgroundColor?: string;
  textColor?: string;
  width: DimensionValue;
  customStyle?: {
    button?: ViewStyle;
    text?: TextStyle;
  };
}

const CustomButton: React.FC<CustomButtonProps> = ({ 
  title, onPress, 
  backgroundColor = "#024059", 
  textColor = "#081828",
  width = "100%",
  customStyle = {} }) => {

  return (
    <TouchableOpacity style={[styles.button, { backgroundColor, width },customStyle.button, customStyle.text  ]} onPress={onPress}>
        <Text style={[styles.buttonText, {color: textColor}]}>{title}</Text>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  button: {
    backgroundColor: "#08c8f8",
    paddingVertical: 12,
    borderRadius: 8,
    alignItems: "center",
    marginBottom: 20,
  },
  buttonText: {
    fontSize: 16,
    fontWeight: "bold",
  },
});

export default CustomButton;
