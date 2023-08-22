using System;
using System.Threading.Tasks;

namespace DenevCloud.Extensions.Types;

/// <summary>
/// Rust-like Option type for C#. Supports implicit conversion to bool, int, byte, uint, sbyte and Option.
/// </summary>
/// <typeparam name="TResult">The type of result that will be returned if the is 'Some' value.</typeparam>
public readonly struct OptionResult<TResult>
{
    private readonly Option _option { get; }
    private readonly TResult? _result { get; }


    /// <summary>
    /// Constructor for a 'Some' OptionResult.
    /// </summary>
    /// <param name="result">The value that will be returned.</param>
    public OptionResult(TResult result)
    {
        _result = result;
        _option = Option.Some;
    }

    /// <summary>
    /// Constructor for a 'None' OptionResult.
    /// </summary>
    public OptionResult()
    {
        _option = Option.None;
    }

    /// <summary>
    /// Return the value of the OptionResult if it is 'Some', otherwise return the default value of the type or throw an exception.
    /// </summary>
    /// <param name="ThrowIfNone">Throw an 'InvalidOperationException' if the Option is 'None'.</param>
    /// <returns>The value stored.</returns>
    /// <exception cref="InvalidOperationException">Throwed if the bool 'ThrowIfNone' is set to true and there is no value stored.</exception>
    public TResult? Unwrap(bool ThrowIfNone = false)
    {
        if (_option == Option.None)
            if(ThrowIfNone)
                throw new InvalidOperationException("Cannot unwrap a None OptionResult.");
            else
                return default;
        else
            return _result;
    }

    /// <summary>
    /// Get the Option of the OptionResult ('Some' or 'None').
    /// </summary>
    public Option Option => _option;

    /// <summary>
    /// Async function for returning the value of the OptionResult if it is 'Some', otherwise return the default value of the type or throw an exception.
    /// </summary>
    /// <param name="ThrowIfNone">Throw an 'InvalidOperationException' if the Option is 'None'.</param>
    /// <returns>The value stored.</returns>
    /// <exception cref="InvalidOperationException">Throwed if the bool 'ThrowIfNone' is set to true and there is no value stored.</exception>
    public Task<TResult?> UnwrapAsync(bool ThrowIfNone = false)
    {
        Task.Yield();

        if (_option == Option.None)
            if (ThrowIfNone)
                throw new InvalidOperationException("Cannot unwrap a None OptionResult.");
            else
                return Task.FromResult<TResult?>(default);
        else
            return Task.FromResult(_result);
    }

    public static implicit operator bool(OptionResult<TResult> result)
    {
        if(result._option == Option.None)
            return false;
        else
            return true;
    }

    public static implicit operator int(OptionResult<TResult> result)
    {
        if (result._option == Option.None)
            return 0;
        else
            return 1;
    }

    public static implicit operator byte(OptionResult<TResult> result)
    {
        if (result._option == Option.None)
            return 0;
        else
            return 1;
    }

    public static implicit operator uint(OptionResult<TResult> result)
    {
        if (result._option == Option.None)
            return 0;
        else
            return 1;
    }

    public static implicit operator sbyte(OptionResult<TResult> result)
    {
        if (result._option == Option.None)
            return 0;
        else
            return 1;
    }

    public static implicit operator Option(OptionResult<TResult> result)
    {
        return result._option;
    }
}

/// <summary>
/// Used to create a 'None' OptionResult.
/// </summary>
/// <typeparam name="TResult">The type of result that will be returned if the is 'Some' value.</typeparam>
public readonly struct None<TResult>
{
    public static implicit operator OptionResult<TResult?>(None<TResult?> none)
    {
        return new OptionResult<TResult?>();
    }
}

/// <summary>
/// Used to create a 'Some' OptionResult. The constructor takes the value that will be returned.
/// </summary>
/// <typeparam name="TResult">The type of result that will be returned if the is 'Some' value.</typeparam>
public readonly struct Some<TResult>
{
    private readonly TResult? _result { get; }

    public Some(TResult? result)
    {
        _result = result;
    }

    public static implicit operator OptionResult<TResult?>(Some<TResult?> some)
    {
        return new OptionResult<TResult?>(some._result);
    }
}

/// <summary>
/// OptionResult<TResult> enum for 'Some' and 'None' type of byte. None = 0, Some = 1.
/// </summary>
public enum Option : byte
{
    None = 0,
    Some = 1
}