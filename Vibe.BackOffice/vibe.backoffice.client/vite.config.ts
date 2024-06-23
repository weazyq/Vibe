import { fileURLToPath, URL } from 'node:url';

import { defineConfig, ServerOptions } from 'vite';
import plugin from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';

const baseFolder =
    process.env.APPDATA !== undefined && process.env.APPDATA !== ''
        ? `${process.env.APPDATA}/ASP.NET/https`
        : `${process.env.HOME}/.aspnet/https`;

const certificateArg = process.argv.map(arg => arg.match(/--name=(?<value>.+)/i)).filter(Boolean)[0];
const certificateName = certificateArg ? certificateArg.groups.value : "vibe.backoffice.client";

if (!certificateName) {
    console.error('Invalid certificate name. Run this script in the context of an npm/yarn script or pass --name=<<app>> explicitly.')
    process.exit(-1);
}

const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    if (0 !== child_process.spawnSync('dotnet', [
        'dev-certs',
        'https',
        '--export-path',
        certFilePath,
        '--format',
        'Pem',
        '--no-password',
    ], { stdio: 'inherit', }).status) {
        throw new Error("Could not create certificate.");
    }
}

const useHttps = process.argv.includes('--https');

const serverConfig: ServerOptions = {
    proxy: {
        '^/Auth': {
            target: 'http://localhost:7221/',
            secure: false
        },
        '^/Employees': {
            target: 'http://localhost:7221/',
            secure: false
        },
        '^/SupportRequests': {
            target: 'http://localhost:7221',
            secure: false
        }
    },
    port: 5173
}

if(useHttps) {
    serverConfig.https = {
        key: fs.readFileSync(keyFilePath),
        cert: fs.readFileSync(certFilePath),
    }
}

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: serverConfig
    // server: {
    //     proxy: {
    //         '^/Auth': {
    //             target: 'https://localhost:7221/',
    //             secure: false
    //         },
    //         '^/Employees': {
    //             target: 'https://localhost:7221/',
    //             secure: false
    //         },
    //         '^/SupportRequests': {
    //             target: 'https://localhost:7221',
    //             secure: false
    //         }
    //     },
    //     port: 5173,
    //     https: {
    //         key: fs.readFileSync(keyFilePath),
    //         cert: fs.readFileSync(certFilePath),
    //     }
    // }
})
