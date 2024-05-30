import { ITypeEnvironment } from "./ITypeEnvironment";

export function getEnvironment(type: ITypeEnvironment): string {
  return type.Local;
}
