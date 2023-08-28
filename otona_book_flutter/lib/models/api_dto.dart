import 'package:json_annotation/json_annotation.dart';

part 'api_dto.g.dart';

@JsonSerializable()
class ApiResponse {
  final int code;
  final dynamic data;
  final String msg;

  ApiResponse({required this.code, this.data, required this.msg});

  factory ApiResponse.fromJson(Map<String, dynamic> json) =>
      _$ApiResponseFromJson(json);
  Map<String, dynamic> toJson() => _$ApiResponseToJson(this);
}
