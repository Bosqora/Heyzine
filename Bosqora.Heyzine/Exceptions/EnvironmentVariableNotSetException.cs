namespace Bosqora.Heyzine.Exceptions;

/// <summary>
/// The exception that is thrown when a required Heyzine environment variable is missing or empty.
/// </summary>
public class EnvironmentVariableNotSetException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnvironmentVariableNotSetException"/> class.
    /// </summary>
    public EnvironmentVariableNotSetException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EnvironmentVariableNotSetException"/> class for the specified variable name.
    /// </summary>
    /// <param name="variableName">The missing environment variable name.</param>
    public EnvironmentVariableNotSetException(string variableName)
        : base(string.Format(Constants.ERRORS_ENVVAR_NOTSET, variableName))
    {
        ArgumentNullException.ThrowIfNull(variableName, nameof(variableName));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EnvironmentVariableNotSetException"/> class for the specified variable name and inner exception.
    /// </summary>
    /// <param name="variableName">The missing environment variable name.</param>
    /// <param name="innerException">The exception that caused the current exception.</param>
    public EnvironmentVariableNotSetException(string variableName, Exception innerException)
        : base(string.Format(Constants.ERRORS_ENVVAR_NOTSET, variableName), innerException)
    {
        ArgumentNullException.ThrowIfNull(variableName, nameof(variableName));
    }
}