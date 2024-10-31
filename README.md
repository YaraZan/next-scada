![image](https://github.com/user-attachments/assets/b67bd205-b835-4445-877f-8c4fd8717668)

# NextSCADA

`NextSCADA` is a cross-platform desktop application built using Vue.js, TypeScript, Electron, and a .NET module for seamless interaction with OPC servers. The application provides an all-in-one SCADA solution for browsing, subscribing to, and managing OPC data in real time, designed for operators and engineers managing industrial automation and control systems.

## Features

- **OPC Server Discovery**: Connect to both local and remote OPC servers, supporting OPC DA and OPC UA protocols.
- **Server Browsing**: Retrieve OPC server nodes, structured as folders or tags, allowing for organized data access.
- **Data Subscription**: Subscribe to OPC tag updates and receive real-time notifications on data changes.
- **WebSocket for Real-Time Updates**: Built-in WebSocket support enables listening to subscribed OPC item updates for high-performance monitoring.
- **Unified Deployment**: Delivered as a single executable for seamless cross-platform distribution.

## Project Structure

The application is divided into two main components:

1. **Client**: Developed with Vue.js, TypeScript, and Electron, providing the user interface and handling all client-side interactions.
2. **Backend Module**: Built in C#, this .NET-based component manages OPC interactions, including server browsing and data subscriptions.

## Building and Running the Project

To set up and build `NextSCADA`, you’ll need to follow these steps:

### Prerequisites

- **Node.js** (>= 14.x)
- **.NET SDK** (>= 5.0)
- **Electron** (cross-platform desktop framework)

### Building Instructions

1. **Compile the .NET Backend Module**:  
   Before building the entire project, compile the backend module with:
   ```bash
   dotnet build -c Release
   ```

2. **Install Client Dependencies**:  
   Navigate to the project root and install dependencies:
   ```bash
   npm install
   ```

3. **Build the Full Application**:  
   Run the Electron build process for a cross-platform desktop application executable:
   ```bash
   npm run compile
   ```

The final executable will be located in the `dist` folder, ready for deployment on Windows, macOS, and Linux.

## Usage

Once launched, `NextSCADA` offers the following functionality:

- **Browse Local and Remote OPC Servers**: Search for accessible OPC servers on the network.
- **Manage OPC Connections**: Easily set up and save connections to local and remote OPC servers.
- **Subscribe to OPC Tag Changes**: Real-time data updates are accessible through WebSocket connections for efficient monitoring.

---

`NextSCADA` provides a modern, user-friendly solution for managing OPC data with speed and flexibility, streamlining SCADA operations in a unified cross-platform environment.
