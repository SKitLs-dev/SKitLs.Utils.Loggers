# SKitLs.Utils.Loggers

This project aims to streamline the logging process and simplify debugging for developers.

## Description

Main purpose of the project is achieved through a well-designed lightweight architecture consisting of three key elements:

1. `enum LogType`:
	
	The LogType enum serves as a central component for categorizing different types of log messages within the project	.
By using this enumeration, developers can easily identify and manage logs based on their specific purposes and significance.
	
	This enhances the overall organization of log data, making it more manageable and accessible.

2. `interface ILogger`:
   
	The ILogger interface provides a standardized mechanism for logging messages throughout the project.					
It abstracts the logging functionality and defines a set of methods and properties that any logging implementation must support.

	This decoupling of logging logic allows developers to switch between different logging implementations without affecting the core functionality of the application.

3. `class DefaultConsoleLogger : ILogger`:
   
	The DefaultConsoleLogger class is a concrete implementation of the ILogger interface, specifically tailored for Console Projects.
It offers a default logging behavior for outputting events and messages to the console.
By leveraging this implementation, developers can quickly enable logging in their Console-based applications without the need for extensive configuration.

By combining these three elements, developers can create a versatile and extendable logging framework that facilitates debugging
and improves overall project maintainability.

The LogType enum categorizes logs, the ILogger interface provides a standardized logging mechanism, and the DefaultConsoleLogger class offers a default logging solution for Console Projects.

This architecture promotes code reusability, modularity, and ease of use, empowering developers to efficiently manage log data and identify and resolve issues during the development process.

## Setup

### Requirements

This project does not have any external dependencies.
It is a self-contained solution that does not require the installation of additional libraries or components.

### Installation

1. Using Terminal Command:
    
    To install the project using the terminal command, follow these steps:

    1. Open the terminal or command prompt.
    2. Run command:
    
    ```
    dotnet add package SKitLs.Utils.Loggers
    ```

2. Using NuGet Packages Manager:

    To install the project using the NuGet Packages Manager, perform the following steps:

    1. Open your preferred Integrated Development Environment (IDE) that supports NuGet package management (e.g., Visual Studio).
    2. Create a new project or open an existing one.
    3. Go to "Tools" > "NuGet Package Manager" > "Manage NuGet Packages for Solution."
    4. In the "Browse" tab, search for the project package you want to install.
    5. Click on the "Install" button to add the selected package to your project.
    5. Follow any additional setup instructions or configurations provided in the project's documentation.

3. Downloading Source Code and Direct Linking:

    To install the project by downloading the source code and directly linking it to your project, adhere to the following steps:

    1. Visit the project repository on [GitHub](https://github.com/Sargeras02/SKitLs.Utils.Loggers.git)
    2. Click on the "Code" button and select "Download ZIP" to download the project's source code as a zip archive.
    3. Extract the downloaded zip archive to the desired location on your local machine.
    4. Open your existing project or create a new one in your IDE.
    5. Add the downloaded project files to your solution using the "Add Existing Project" option in your IDE's solution explorer.
    6. Reference the project in your solution and ensure any required dependencies are resolved.
    7. Follow any additional setup or configuration instructions provided in the project's documentation.

Please note that each method may have specific requirements or configurations that need to be followed for successful installation.
Refer to the project's documentation for any additional steps or considerations.

## Usage

### Enhancing Console-Based Logging:

For Console-based projects, you can take advantage of the DefaultConsoleLogger class:

1. Initialize the DefaultConsoleLogger:

    ```C#
    ILogger logger = new DefaultConsoleLogger();
    ```

2. Logging Messages to the Console:

    ```C#
    logger.Log("This is an informational message.", LogType.Info);
    logger.Log("Warning: Potential issue detected!", LogType.Warning);
    logger.Log($"Error occurred: {exception.Message}", LogType.Error);
    // or
    logger.Warn("Warning: Potential issue detected!");
    logger.Error($"Error occurred: {exception.Message}");
    ```

### Customizing Logging Behavior:

If you need to customize the logging behavior based on your project's specific requirements,
implement the ILogger interface in a custom class:

1. Create a Custom Logger Class:

    ```C#
    public class CustomLogger : ILogger
    {
        // Implement the methods from the ILogger interface as per your custom logging needs.
        // Example: You can log messages to a file, database, or an external service.
    }
    ```
2. Initialize Your Custom Logger:

    ```C#
    ILogger logger = new CustomLogger(); // Use your custom logger to handle logging in your project.
    ```

3. Log Your Messages:

    ```C#
    logger.Log("This is an informational message.", LogType.Info);
    logger.Warn("Warning: Potential issue detected!");
    logger.Error($"Error occurred: {exception.Message}");
    ```

## Contributors

Currently, there are no contributors actively involved in this project.
However, our team is eager to welcome contributions from anyone interested in advancing the project's development.

We value every contribution and look forward to collaborating with individuals who share our vision and passion for this endeavor.
Your participation will be greatly appreciated in moving the project forward.

Thank you for considering contributing to our project.

## License

This project is distributed under the terms of the MIT License.

Copyright (C) Sargeras02 2023

## Developer contact

For any issues related to the project, please feel free to reach out to us through the project's GitHub page.
We welcome bug reports, feedback, and any other inquiries that can help us improve the project.

You can also contact the project owner directly via their GitHub profile at the following [link](https://github.com/Sargeras02).

Your collaboration and support are highly appreciated, and we will do our best to address any concerns or questions promptly and professionally.
Thank you for your interest in our project.

## Notes

This project is utilized by a larger and well-established project, which can be found at the following [link](https://github.com/Sargeras02/SKitLs.Bots.Telegram.git).

Thank you for choosing our solution for your needs, and we look forward to contributing to your project's success.
