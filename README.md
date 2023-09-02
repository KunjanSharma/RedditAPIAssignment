# RedditAPIAssignment
# Reddit API Assignment Documentation

## Introduction

The Reddit API Assignment is a comprehensive solution designed to fetch and process data from Reddit's API, particularly focusing on posts from subreddits. The application showcases various features, from rate limiting and concurrency to applying SOLID principles and ensuring secure access.

## Projects Overview

### 1. Console Application

The Console Application serves as the user's primary interaction point. It's designed to display the fetched results from Reddit, providing a quick and straightforward interface for users to see the processed data.

### 2. WebApi

The WebApi project houses the core functionalities related to the Reddit API calls. It acts as the main point of contact between our solution and Reddit's servers. This project also implements rate limiting and other crucial features ensuring optimal and safe communication with Reddit's endpoints.

### 3. UnitTest Project

A testament to the application's robustness, the UnitTest project contains a suite of tests designed to ensure that each functionality works as intended. Using the Moq framework, the tests simulate various scenarios to validate the codebase.

## Features

### Rate Limiting

Rate limiting ensures that our application respects Reddit's guidelines concerning the frequency of API calls. This feature is achieved using the RateLimitedApiClient, which adjusts the request rate based on the response headers provided by the Reddit API.

### Concurrency

Concurrency ensures that the application remains responsive and can handle multiple tasks efficiently. By leveraging `async/await` and multithreading, the system processes posts concurrently, making optimal use of computing resources.

### Scalability

The design emphasizes scalability. By utilizing a microservice architecture and dependency injection in the WebAPI controllers, the system can easily be scaled up to handle more requests or be adapted to manage data from multiple subreddits.

### SOLID Principles

By employing interface inheritance, the solution follows the SOLID principles, ensuring a modular and maintainable codebase.

### Error Handling

To provide stability and reliability, error handling is meticulously implemented throughout the Console Application using try-catch blocks, ensuring that potential issues are caught, logged, and handled gracefully.

### OAuth and Security

The application uses OAuth for securely accessing Reddit's API. The API credentials are fetched from the `RedditPostAssignment/appsettings.json` under the "RedditCredentials" section.

## Configuration

### Setting Up API Token:

1. Navigate to the `RedditPostAssignment/appsettings.json`.
2. Find the "RedditCredentials" section.
3. Fill in your Reddit API credentials:
    ```json
    "RedditCredentials": {
        "ClientId": "YOUR_CLIENT_ID",
        "ClientSecret": "YOUR_CLIENT_SECRET",
        "Username": "YOUR_REDDIT_USERNAME",
        "Password": "YOUR_REDDIT_PASSWORD"
    }
    ```

**Note**: Always prioritize security when dealing with sensitive information. Ensure that the `appsettings.json` file is kept secure and away from public repositories.

### Targeted Subreddit

Currently, the application is coded to fetch information from the "worldnews" subreddit. However, the architecture allows for easy adaptability to other subreddits or even multiple ones in future iterations.

---

This document serves as an overview of the implemented features and projects. For further details or adjustments, always refer to the provided codebase and associated comments.
