﻿namespace Docx.Automation;
/// <summary>
/// Specifies a value that determines the Input Method Editor (IME) status of an object when the object is selected.
/// </summary>
public enum IMEMode
{
  /// <summary>
  /// Inherits the IME mode of the owner control.
  /// </summary>
  Inherit = -1,
  /// <summary>
  /// None (Default).
  /// </summary>
  NoControl = 0,
  /// <summary>
  /// The IME is on. This value indicates that the IME is on and characters specific to Chinese or Japanese can be entered. This setting is valid for Japanese, Simplified Chinese, and Traditional Chinese IME only.
  /// </summary>
  On = 1,
  /// <summary>
  /// The IME is off. This mode indicates that the IME is off, meaning that the object behaves the same as English entry mode. This setting is valid for Japanese, Simplified Chinese, and Traditional Chinese IME only.
  /// </summary>
  Off = 2,
  /// <summary>
  /// The IME is disabled. With this setting, the users cannot turn the IME on from the keyboard, and the IME floating window is hidden.
  /// </summary>
  Disable = 3,
  /// <summary>
  /// Hiragana DBC. This setting is valid for the Japanese IME only.
  /// </summary>
  Hiragana = 4,
  /// <summary>
  /// Katakana DBC. This setting is valid for the Japanese IME only.
  /// </summary>
  Katakana = 5,
  /// <summary>
  /// Katakana SBC. This setting is valid for the Japanese IME only.
  /// </summary>
  KatakanaHalf = 6,
  /// <summary>
  /// Alphanumeric double-byte characters. This setting is valid for Korean and Japanese IME only.
  /// </summary>
  AlphaFull = 7,
  /// <summary>
  /// Alphanumeric single-byte characters(SBC). This setting is valid for Korean and Japanese IME only.
  /// </summary>
  Alpha = 8,
  /// <summary>
  /// Hangul DBC. This setting is valid for the Korean IME only.
  /// </summary>
  HangulFull = 9,
  /// <summary>
  /// Hangul SBC. This setting is valid for the Korean IME only.
  /// </summary>
  Hangul = 10,
  /// <summary>
  /// IME closed. This setting is valid for Chinese IME only.
  /// </summary>
  Close = 11,
  /// IME on HalfShape. This setting is valid for Chinese IME only.
  /// <summary>
  /// </summary>
  OnHalf = 12,

}
