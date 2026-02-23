import { existsSync } from 'fs';
import { join, dirname } from 'path';
import { execSync } from 'child_process';
import os from 'os';
import { fileURLToPath } from 'url';

const __dirname = dirname(fileURLToPath(import.meta.url));
const platform = os.platform();
const isWindows = platform === 'win32';
const binaryName = isWindows ? 'router.exe' : 'router';
const binaryPath = join(__dirname, binaryName);

if (!existsSync(binaryPath)) {
    console.log(`\x1b[36m[Setup] Apollo Router binary not found. Downloading for ${platform}...\x1b[0m`);

    try {
        if (isWindows) {
            execSync(`powershell -Command "Invoke-WebRequest -Uri https://router.apollo.dev/download/win/latest -OutFile '${binaryPath}'"`, { stdio: 'inherit' });
        } else {
            execSync(`curl -sSL https://router.apollo.dev/download/nix/latest | sh`, { cwd: __dirname, stdio: 'inherit' });
        }
        console.log(`\x1b[32m[Setup] Download complete: ${binaryPath}\x1b[0m`);
    } catch (error) {
        console.error(`\x1b[31m[Setup] Failed to download Apollo Router: ${error.message}\x1b[0m`);
        process.exit(1);
    }
}
