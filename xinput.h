typedef struct _XINPUT_GAMEPAD {
  WORD  wButtons;
  BYTE  bLeftTrigger;
  BYTE  bRightTrigger;
  SHORT sThumbLX;
  SHORT sThumbLY;
  SHORT sThumbRX;
  SHORT sThumbRY;
} XINPUT_GAMEPAD, *PXINPUT_GAMEPAD;

void XInputEnable(
  BOOL enable
);

DWORD XInputGetKeystroke(
  DWORD             dwUserIndex,
  DWORD             dwReserved,
  PXINPUT_KEYSTROKE pKeystroke
);

DWORD XInputGetState(
  DWORD        dwUserIndex,
  XINPUT_STATE *pState
);

DWORD XInputSetState(
  DWORD            dwUserIndex,
  XINPUT_VIBRATION *pVibration
);

typedef struct _XINPUT_CAPABILITIES {
  BYTE             Type;
  BYTE             SubType;
  WORD             Flags;
  XINPUT_GAMEPAD   Gamepad;
  XINPUT_VIBRATION Vibration;
} XINPUT_CAPABILITIES, *PXINPUT_CAPABILITIES;
