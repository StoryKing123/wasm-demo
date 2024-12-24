import { dotnet } from "./_framework/dotnet.js";
let exportsPromise = null;

const createRuntimeAndGetExports = async () => {
  const { getAssemblyExports, getConfig } = await dotnet.create();
  const config = getConfig();
  return await getAssemblyExports(config.mainAssemblyName);
};


export async function decodeImage(url) {
  if (!exportsPromise) {
    exportsPromise = createRuntimeAndGetExports();
  }
  const exports = await exportsPromise;
  return exports.JSBridge.DecodeImage(url);
}

